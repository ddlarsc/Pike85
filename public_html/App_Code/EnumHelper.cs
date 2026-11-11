using System;
using System.ComponentModel;
using System.Reflection;
using System.Web.UI.WebControls;

namespace App_Code
{
    public static class EnumHelper
    {
        public static string GetDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute),false);
            if(attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            return string.Empty;
        }

        /// <summary>
        /// Name is the visibel part = SelectedItem.
        /// Description is the invisible part = SeletedValue.
        /// </summary>
        public static ListItem[] GetNameDescriptionPair(Type enumType)
        {
            var items = new ListItem[enumType.GetFields().Length - 1];
            for(int idx = 0;idx < enumType.GetFields().Length - 1;idx++)
            {
                var fi = (FieldInfo)enumType.GetFields().GetValue(idx + 1);
                string name = fi.Name;

                var descriptionAttributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute),false);
                string description = String.Empty;
                if(descriptionAttributes.Length > 0)
                {
                    description = descriptionAttributes[0].Description;
                }

                var item = new ListItem(name,description);
                items.SetValue(item,idx);
            }
            return items;
        }

        /// <summary>
        /// Description is the visibel part = SelectedItem.
        /// Name is the invisible part = SeletedValue.
        /// </summary>
        public static ListItem[] GetDescriptionNamePair(Type enumType)
        {
            var items = new ListItem[enumType.GetFields().Length - 1];
            for(int idx = 0;idx < enumType.GetFields().Length - 1;idx++)
            {
                var fi = (FieldInfo)enumType.GetFields().GetValue(idx + 1);
                string name = fi.Name;

                var descriptionAttributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute),false);
                string description = string.Empty;
                if(descriptionAttributes.Length > 0)
                {
                    description = descriptionAttributes[0].Description;
                }

                var item = new ListItem(description,name);
                items.SetValue(item,idx);
            }
            return items;
        }

        /// <summary>
        /// Binding a DropDownList to a Enum
        /// Description is the visibel part = SelectedItem.
        /// Name is the invisible part = SeletedValue.
        /// </summary>
        /// <param name="dropDownList">Reference to a specific DropDownList thats going to be filled witg data.</param>
        /// <param name="type">Referece to a specific Enum list that the DropDownList is binding to.</param>
        public static void LoadDescriptionNameIntoDropDownList(DropDownList dropDownList, Type type)
        {
            ListItem[] descriptionNamePair = GetDescriptionNamePair(type);
            dropDownList.Dispose();
            dropDownList.DataSource = descriptionNamePair;
            dropDownList.DataBind();
        }

        /// <summary>
        /// Binding a DropDownList to a Enum
        /// Name is the visibel part = SelectedItem.
        /// Description is the invisible part = SeletedValue.
        /// </summary>
        /// <param name="dropDownList">Reference to a specific DropDownList thats going to be filled witg data.</param>
        /// <param name="type">Referece to a specific Enum list that the DropDownList is binding to.</param>
        public static void LoadNameDescriptionIntoDropDownList(DropDownList dropDownList,Type type)
        {
            ListItem[] descriptionNamePair = GetNameDescriptionPair(type);
            dropDownList.Dispose();
            dropDownList.DataSource = descriptionNamePair;
            dropDownList.DataBind();
        }

    }
}