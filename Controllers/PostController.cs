using System;
using System.Reflection;
using System.Web.Mvc;
using CodeEffects.Rule.Core;
using CodeEffects.Rule.Models;
using CodeEffects.Rule.Demo.Bre.Mvc.Models;
using CodeEffects.Rule.Demo.Bre.Mvc.Services;

namespace CodeEffects.Rule.Demo.Bre.Mvc.Controllers
{
    public class PostController : Controller
    {
        public PostController()
        {
            ViewBag.Message = "Demo version. Evaluations are delayed for 1 second.";

            // Storing data for the Gender drop down declared in the /Views/Shared/_PatientForm.cshtml shared view
            ViewBag.Groups = DataService.GetGroups(true);

            //// Storing data for the State drop downs
            //ViewBag.States = DataService.GetStates(false);

            //// Storing data for the EducationLevel drop down
            //ViewBag.EducationLevels = DataService.GetEducationLevels();

            //// Storing data for the Physician drop downs
            //ViewBag.Physicians = DataService.GetPhysicians();

            this.LoadMenuRules();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Rule = RuleModel.Create(typeof(Order));
            return View();
        }

        // "ruleEditor" is the ID of the RuleEditor instance declared in the view.
        // The name of the action parameter must match that ID, even though RuleEditor
        // and RuleModel are two different types. This is MVC's default behavior.
        [HttpPost]
        public ActionResult Save(RuleModel ruleEditor)
        {
            // At this point the rule model doesn't know which type to use as its source object
            // We need to "bind" the source type to the rule model
            ruleEditor.BindSource(typeof(Order));

            // Add the rule model to the ViewBag object
            ViewBag.Rule = ruleEditor;

            // Make sure that the Rule Area is not empty and the current rule is valid
            if (ruleEditor.IsEmpty() || !ruleEditor.IsValid(StorageService.LoadRuleXml))
            {
                ViewBag.Message = "The rule is empty or invalid";
                return View("Index");
            }

            try
            {
                // Save the rule
                StorageService.SaveRule(
                    ruleEditor.Id,
                    ruleEditor.GetRuleXml(),
                    ruleEditor.IsLoadedRuleOfEvalType == null ?
                        true : (bool)ruleEditor.IsLoadedRuleOfEvalType);

                // Get all rules for Tool Bar and context menus and save it in the bag
                this.LoadMenuRules();

                ViewBag.Message = "The rule was saved successfully";
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return View("Index");
        }

        [HttpGet]
        public ActionResult Load(string id)
        {
            // Load rule from the storage
            string ruleXml = StorageService.LoadRuleXml(id);

            // Create a new model and store it in the bag
            ViewBag.Rule = RuleModel.Create(ruleXml, typeof(Order));

            ViewBag.Message = "The rule is loaded";
            return View("Index");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            // Create a new model and store it in the bag
            ViewBag.Rule = RuleModel.Create(typeof(Order));

            try
            {
                // Delete the rule by its ID
                StorageService.DeleteRule(id);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Index");
            }

            // Refresh Tool Bar and context menus
            this.LoadMenuRules();

            ViewBag.Message = "The rule was deleted successfully";
            return View("Index");
        }

        [HttpPost]
        public ActionResult Evaluate(Order order, RuleModel ruleEditor)
        {
            // Tell Code Effects which type to use as its source object
            ruleEditor.BindSource(typeof(Order));

            // We are not saving the rule, just evaluating it. Tell the model not to enforce the rule name validation
            ruleEditor.SkipNameValidation = true;
            ViewBag.Rule = ruleEditor;

            if (ruleEditor.IsEmpty() || !ruleEditor.IsValid(StorageService.LoadRuleXml))
            {
                ViewBag.Message = "The rule is empty or invalid";
                return View("Index", order);
            }

            // Clear the state (in case value setters are used in this
            // rule and we want to display the new values in the form)
            this.ClearState(typeof(Order), string.Empty);

            // Get rule XML
            string rule = ruleEditor.GetRuleXml();

            // Create an instance of the Evaluator class. Because our rules might reference other rules of evaluation type
            // we use constructor that takes rule's XML and delegate of the method that can load referenced rules by their IDs.
            Evaluator<Order> evaluator = new Evaluator<Order>(rule, StorageService.LoadRuleXml);
            // Evaluate the patient against the rule
            bool success = evaluator.Evaluate(order);

            //Test chaining rules
            //string id = "70a7fde6-5bf1-460f-b885-e56f95a9f844";
            //string rule2 = StorageService.LoadRuleXml(id);
            //Evaluator<Order> evaluator2 = new Evaluator<Order>(rule2, StorageService.LoadRuleXml);
            //bool success2 = evaluator2.Evaluate(order);

            //if(!string.IsNullOrWhiteSpace(order.Output))
            //	ViewBag.Message = order.Output;
            //else
            ViewBag.Message = "The current rule evaluated to " + success;

            return View("Index", order);
        }

        // MVC looks for posted values in our form first. We need to clear those
        // values in order to make the shared view to display new parient values.
        // This is MVC's helpers default behavior, it has nothing to do with Code Effects
        private void ClearState(Type type, string prefix)
        {
            foreach (PropertyInfo pi in type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public))
                ModelState.Remove(prefix + pi.Name);
        }

        private void LoadMenuRules()
        {
            ViewBag.ToolBarRules = StorageService.GetAllRules();
            ViewBag.ContextMenuRules = StorageService.GetEvaluationRules();
        }
    }
}