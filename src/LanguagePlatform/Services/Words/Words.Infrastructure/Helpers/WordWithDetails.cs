using Words.Domain.Entities;

namespace Words.Infrastructure.Helpers;

public class WordWithDetails
{
    public required Word Word { get; set;}

    
    public required Category Category { get; set; }

    
    public required Level Level { get; set; }

    public required TranslatedWord TranslatedWord { get; set; }
}
