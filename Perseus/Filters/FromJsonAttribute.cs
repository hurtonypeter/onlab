using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Script.Serialization;


namespace Perseus.Filters
{
    //public class FromJsonAttribute : CustomModelBinderAttribute
    //{
    //    private readonly static JavaScriptSerializer serializer = new JavaScriptSerializer();

    //    public override IModelBinder GetBinder()
    //    {
    //        return new JsonModelBinder();
    //    }

    //    private class JsonModelBinder : IModelBinder
    //    {
    //        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    //        {
    //            var stringified = controllerContext.HttpContext.Request[bindingContext.ModelName];
    //            if (string.IsNullOrEmpty(stringified))
    //                return null;
    //            var model = serializer.Deserialize(stringified, bindingContext.ModelType);

    //            // DataAnnotation Validation
    //            var validationResult = from prop in TypeDescriptor.GetProperties(model).Cast<PropertyDescriptor>()
    //                                   from attribute in prop.Attributes.OfType<ValidationAttribute>()
    //                                   where !attribute.IsValid(prop.GetValue(model))
    //                                   select new { Propertie = prop.Name, ErrorMessage = attribute.FormatErrorMessage(string.Empty) };

    //            // Add the ValidationResult's to the ModelState
    //            foreach (var validationResultItem in validationResult)
    //                bindingContext.ModelState.AddModelError(validationResultItem.Propertie, validationResultItem.ErrorMessage);

    //            return model;
    //        }

            
    //    }
    //}

    public class FromJsonAttribute : CustomModelBinderAttribute
    {
        private readonly static JavaScriptSerializer serializer = new JavaScriptSerializer();

        public override IModelBinder GetBinder()
        {
            return new JsonModelBinder();
        }

        private class JsonModelBinder : IModelBinder
        {
            public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
                var stringified = controllerContext.HttpContext.Request[bindingContext.ModelName];
                if (string.IsNullOrEmpty(stringified))
                    return null;
                return serializer.Deserialize(stringified, bindingContext.ModelType);
            }
        }
    }
}