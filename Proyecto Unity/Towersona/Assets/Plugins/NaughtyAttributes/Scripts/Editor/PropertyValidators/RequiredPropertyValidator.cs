using UnityEditor;

namespace NaughtyAttributes.Editor
{
    [PropertyValidator(typeof(RequiredAttribute))]
    public class RequiredPropertyValidator : PropertyValidator
    {
        public override void ValidateProperty(SerializedProperty property)
        {
            RequiredAttribute requiredAttribute = PropertyUtility.GetAttribute<RequiredAttribute>(property);

            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                if (property.objectReferenceValue == null)
                {
                    string errorMessage = property.name + " is required";
                    if (!string.IsNullOrEmpty(requiredAttribute.Message))
                    {
                        errorMessage = requiredAttribute.Message;
                    }

                    bool shouldLogToConsole = requiredAttribute.LogToConsole;

                    EditorDrawUtility.DrawHelpBox(errorMessage, MessageType.Error, logToConsole: shouldLogToConsole, context: PropertyUtility.GetTargetObject(property));
                }
            }
            else
            {
                string warning = requiredAttribute.GetType().Name + " works only on reference types";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, logToConsole: true, context: PropertyUtility.GetTargetObject(property));
            }
        }
    }
}
