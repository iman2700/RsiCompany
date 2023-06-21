using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RsiCompany.Presentation
{
    // This class defines a custom model binder that can be used to bind arrays from HTTP requests to objects in memory.
    public class ArrayModelBinder : IModelBinder
    {
        // This method is called by the framework to bind a model property from an HTTP request to an object in memory.
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {


            // Check if the model property is an array type.
            if (!bindingContext.ModelMetadata.IsEnumerableType)
            {
                // If the model property is not an array type, return a ModelBindingResult object with a ModelState.Failed error.
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }
            // Get the value of the model property from the HTTP request.
            var providedValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString();

            // Check if the value of the model property is empty.
            if (string.IsNullOrEmpty(providedValue))
            {
                // If the value of the model property is empty, return a ModelBindingResult object with a ModelState.Success error.
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask; 
            }

            // Get the generic type of the model property.
            var genericType = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];

            // Get a converter for the generic type of the model property.
            var converter = TypeDescriptor.GetConverter(genericType);

            // Split the value of the model property into an array of strings.
            var objectArray = providedValue.Split(new[] { "," },
                StringSplitOptions.RemoveEmptyEntries).Select(x => converter.ConvertFromString(x.Trim())).ToArray();

            // Create a new array of the generic type.
            var guidArray = Array.CreateInstance(genericType, objectArray.Length);

            // Copy the values from the array of strings to the new array.
            objectArray.CopyTo(guidArray, 0);

            // Set the value of the model property to the new array.
            bindingContext.Model = guidArray;

            // Return a ModelBindingResult object with a ModelState.Success error.
            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            return Task.CompletedTask;
        }

    
}
}
