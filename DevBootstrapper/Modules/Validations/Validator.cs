using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevBootstrapper.Application;
using DevMvcComponent.Error;

namespace DevBootstrapper.Modules.Validations {
    public abstract class Validator {
        protected ErrorCollector ErrorCollector;
        protected delegate bool RunValidation();
        protected List<RunValidation> ValidationCollection;
        /// <summary>
        /// On initialization CollectValidation() is called to collect all the validations.
        /// </summary>
        /// <param name="errorCollector"></param>
        /// <param name="capacity"></param>
        protected Validator(ErrorCollector errorCollector = null, int capacity = 25) {
            if (errorCollector == null) {
                errorCollector = AppConfig.GetNewErrorCollector();
            }
            ErrorCollector = errorCollector;
            ValidationCollection = new List<RunValidation>(capacity);

            CollectValidation();
        }

        protected void AddValidation(RunValidation validation) {
            ValidationCollection.Add(validation);
        }

        protected void ClearValidation() {
            ValidationCollection.Clear();
        }

        /// <summary>
        /// In this method all the  
        /// validation methods 
        /// should be added to the 
        /// collection via AddValidation() method.
        /// Returns true means validation is correct.
        /// </summary>
        public abstract void CollectValidation();
        /// <summary>
        /// Run all the validation methods and then 
        /// set the ErrorCollector for the session.
        /// </summary>
        /// <returns>Returns true if no error exist</returns>
        public bool FinalizeValidation() {
            bool anyValidationErrorExist = false;
            foreach (var action in ValidationCollection) {
                if (!anyValidationErrorExist) {
                    anyValidationErrorExist = !action();
                }
            }
            if (anyValidationErrorExist) {
                AppConfig.SetGlobalError(ErrorCollector);
            }
            return !anyValidationErrorExist;
        }
    }
}