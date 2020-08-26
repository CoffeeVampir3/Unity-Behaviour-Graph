using System;
using System.Reflection;
using BehaviourGraph.Conditionals;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BehaviourGraph.Blackboard
{
    [Serializable]
    public class ConditionalSelector
    {
        [NonSerialized, OdinSerialize]
        [ValueDropdown("GetDropdownListOfClassesWithConditions")]
        public Type conditionClassSelector;
        
        [HideIf("ShowSelectors")]
        [OnValueChanged("SelectorTypeSwapped")]
        public bool isMethod = true;
        
        [HideIf("ShowMethods"), HideIf("ShowSelectors")]
        [ValueDropdown("GetDropdownOfClassSpecificMethodConditions")]
        public MethodInfo methodSelector;
        

        [HideIf("ShowFields"), HideIf("ShowSelectors")]
        [ValueDropdown("GetDropdownOfClassSpecificFieldConditions")]
        public int fieldSelector;

        public void Validate(BlackboardReference parentReference)
        {
            if (isMethod)
            {
                if (methodSelector == null)
                {
                    Debug.LogError(parentReference.name + " has indicated it has a " +
                                   "method condition but does not have a valid method selected.");
                }
            }
            else
            {
                if (fieldSelector == -1)
                {
                    Debug.LogError(parentReference.name + " has indicated it has a " +
                                   "field condition but does not have a valid field selected.");
                }
            }
        }

        #region OdinDisplayStuff
        
        private bool ShowSelectors => conditionClassSelector == null;
        
        private void SelectorTypeSwapped()
        {
            methodSelector = null;
            fieldSelector = -1;
        }

        public bool TryGetConditionDisplayValue(out string outputString)
        {
            outputString = "";
            if (conditionClassSelector == null)
            {
                return false;
            }
            outputString += conditionClassSelector.Name + ", ";
            if (isMethod)
            {
                if (methodSelector == null)
                {
                    return false;
                }
                outputString += methodSelector.Name;
            }
            else
            {
                if (!ConditionalCache.TryGetConditionsFor(conditionClassSelector, out FieldInfo[] fields))
                {
                    return false;
                }
                if (fields[fieldSelector] == null)
                {
                    return false;
                }
                outputString += fields[fieldSelector].Name; 
            }

            return true;
        }

        private bool ShowMethods => !isMethod;
        private bool ShowFields => isMethod;

        private ValueDropdownList<Type> GetDropdownListOfClassesWithConditions() {
            var filterList = new ValueDropdownList<Type>();
            var typeList = ConditionalCache.ClassesWithCondition;

            foreach (var listedType in typeList) { 
                filterList.Add(listedType.Name, listedType);
            }
            filterList.Sort((val1, val2) => String.Compare(val1.Text, val2.Text, StringComparison.Ordinal));
            return filterList;
        }
        
        private static ValueDropdownList<MethodInfo> methodSelectorDropdown;
        private Type previouslySelectedMethodType;
        private ValueDropdownList<MethodInfo> GetDropdownOfClassSpecificMethodConditions()
        {
            if (conditionClassSelector == null)
            {
                return null;
            }

            if (previouslySelectedMethodType != conditionClassSelector)
            {
                methodSelectorDropdown = null;
            }

            if (methodSelectorDropdown == null)
            {
                previouslySelectedMethodType = conditionClassSelector;
                methodSelectorDropdown = new ValueDropdownList<MethodInfo>();
                if (ConditionalCache.TryGetConditionsFor(conditionClassSelector, out MethodInfo[] methods))
                {
                    foreach (var method in methods) {
                        methodSelectorDropdown.Add(method.Name, method);
                    }
                }
            }

            return methodSelectorDropdown;
        }
        
        private Type previouslySelectedFieldType;
        private static ValueDropdownList<int> fieldSelectorDropdown;
        private ValueDropdownList<int> GetDropdownOfClassSpecificFieldConditions()
        {
            if (conditionClassSelector == null)
                return null;

            if (previouslySelectedFieldType != conditionClassSelector)
            {
                fieldSelectorDropdown = null;
            }

            if (fieldSelectorDropdown == null)
            {
                previouslySelectedFieldType = conditionClassSelector;
                fieldSelectorDropdown = new ValueDropdownList<int>();
                if (ConditionalCache.TryGetConditionsFor(conditionClassSelector, out FieldInfo[] fields))
                {
                    for (int i = 0; i < fields.Length; i++)
                    {
                        fieldSelectorDropdown.Add(fields[i].Name, i);
                    }
                }
            }

            return fieldSelectorDropdown;
        }
        
        #endregion
        
    }
}