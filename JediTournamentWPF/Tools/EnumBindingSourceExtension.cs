using System;
using System.Windows.Markup;

namespace JediTournamentWPF.Tools
{
    public class EnumBindingSourceExtension : MarkupExtension
    {
        /// <summary>
        /// Type de l'enumération à encapsuler.
        /// </summary>
        private Type _enumType;

        /// <summary>
        /// Propriété permettant de récupérer ou changer le type d'énum utilisé.
        /// </summary>
        public Type EnumType
        {
            get { return this._enumType; }
            set
            {
                if (value != this._enumType)
                {
                    if (null != value)
                    {
                        Type enumType = Nullable.GetUnderlyingType(value) ?? value;

                        // Vérifie que ce type est bien une enum.
                        if (!enumType.IsEnum)
                            throw new ArgumentException("Type must be for an Enum.");
                    }

                    this._enumType = value;
                }
            }
        }

        /// <summary>
        /// Constructeur.
        /// </summary>
        public EnumBindingSourceExtension()
        {
        }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="enumType">Type de l'énumération à utiliser.</param>
        public EnumBindingSourceExtension(Type enumType)
        {
            this.EnumType = enumType;
        }

        /// <summary>
        /// Fournit une valeur correspondant à l'énumération.
        /// </summary>
        /// <returns>Tableau des valeurs de l'énumération (Bindable).</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (null == this._enumType)
                throw new InvalidOperationException("The EnumType must be specified.");

            Type actualEnumType = Nullable.GetUnderlyingType(this._enumType) ?? this._enumType;
            Array enumValues = Enum.GetValues(actualEnumType);

            if (actualEnumType == this._enumType)
                return enumValues;

            Array tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
            enumValues.CopyTo(tempArray, 1);
            return tempArray;
        }
    }
}
