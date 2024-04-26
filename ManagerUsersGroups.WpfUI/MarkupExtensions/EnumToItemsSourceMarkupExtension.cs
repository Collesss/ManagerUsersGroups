using System;
using System.Windows.Markup;

namespace ManagerUsersGroups.WpfUI.MarkupExtensions
{
    class EnumToItemsSourceMarkupExtension : MarkupExtension
    {
        private readonly Type _type;

        public EnumToItemsSourceMarkupExtension(Type type) 
        {
            _type = type;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) =>
            Enum.GetValues(_type);
    }
}
