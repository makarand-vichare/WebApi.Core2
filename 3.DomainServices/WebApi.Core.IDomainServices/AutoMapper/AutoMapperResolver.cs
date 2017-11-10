using AutoMapper;
using System;
using System.Globalization;
using System.Linq;
using WebApi.Core.EntityModels.Identity;
using WebApi.Core.Utility;
using WebApi.Core.ViewModels.Identity.WebApi;

namespace WebApi.Core.IDomainServices.AutoMapper
{
    public class NationalityResolver : IValueResolver<int, string, string>
    {
        public string Resolve(int source, string destination, string destMember, ResolutionContext context)
        {
            if (source > 0)
            {
                Nationality enumVal = (Nationality)Enum.Parse(typeof(Nationality), source.ToString());
                return enumVal.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }

    class CommaSeparatedListResolver : IValueResolver<string, string[], string[]>
    {
        public string[] Resolve(string source, string[] destination, string[] destMember, ResolutionContext context)
        {
            return (source ?? "").Split(',').Select(item => item.Trim()).Where(item => !string.IsNullOrEmpty(item)).OrderBy(item => item).Distinct().ToArray();
        }

    }

    public class GenderResolver : IValueResolver<int, string, string>
    {
        public string Resolve(int source, string destination, string destMember, ResolutionContext context)
        {
            if (source > 0)
            {
                Gender enumVal = (Gender)Enum.Parse(typeof(Gender), source.ToString());
                return enumVal.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }

    public class ApplicationTypeEnumResolver : IMemberValueResolver<Client, ClientViewModel, int , ApplicationTypes>
    {
        public ApplicationTypes Resolve(Client source, ClientViewModel destination, int sourceMember, ApplicationTypes destMember, ResolutionContext context)
        {
            if (source.ApplicationType > 0)
            {
                return (ApplicationTypes)Enum.Parse(typeof(ApplicationTypes), source.ApplicationType.ToString());
            }
            else
            {
                return ApplicationTypes.None;
            }
        }
    }

    public class ApplicationTypeIntResolver : IMemberValueResolver<ClientViewModel, Client, ApplicationTypes, int>
    {
        public int Resolve(ClientViewModel source, Client destination, ApplicationTypes sourceMember, int destMember, ResolutionContext context)
        {
            return (int)source.ApplicationType;
        }
    }

    public class DateResolver : IValueResolver<string, DateTime?, DateTime?>
    {
        public DateTime? Resolve(string source, DateTime? destination, DateTime? destMember, ResolutionContext context)
        {
            try
            {
                return DateTime.ParseExact(source, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            catch (ArgumentNullException ex)
            {
                return null;
            }
            catch (FormatException ex)
            {
                return null;
            }
        }
    }
}
