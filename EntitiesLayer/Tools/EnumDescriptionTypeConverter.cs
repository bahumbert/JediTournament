using System;
using System.ComponentModel;
using System.Reflection;

namespace EntitiesLayer.Tools
{
    public class EnumDescriptionTypeConverter : EnumConverter
    {
        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="type">Type de l'énumération à convertir.</param>
        public EnumDescriptionTypeConverter(Type type)
            : base(type)
        {
        }

        /// <summary>
        /// Conversion du type de l'énumération.
        /// </summary>
        /// <returns>Valeur de retour après conversion de la valeur de l'énumération.</returns>
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value != null)
                {
                    // Récupère le nom de valeur de l'énum
                    FieldInfo fi = value.GetType().GetField(value.ToString());
                    if (fi != null)
                    {
                        // Récupère la description apportée à la valeur en question si possible
                        var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                        return ((attributes.Length > 0) && (!String.IsNullOrEmpty(attributes[0].Description))) ? attributes[0].Description : value.ToString();
                    }
                }

                return string.Empty;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
