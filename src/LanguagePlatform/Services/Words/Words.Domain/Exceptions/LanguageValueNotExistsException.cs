using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Words.Domain.Exceptions
{
    public class LanguageValueNotExistsException : Exception
    {
        public LanguageValueNotExistsException(int languageId) : base($"Language not exist in schema. Selected LanguageId is: {languageId}")
        {
            
        }
    }
}
