using PropertyChanged;
using System.Collections.ObjectModel;
using System.Linq;

namespace Test
{
    [AddINotifyPropertyChangedInterface]
    public class IntChars
    {
        public IntChars(int @int) => Value=@int;

        private int Value { get; set; } = 1;
        public int INT { get => Value; set => Value=value; }
        public char CHAR { get => (char)Value; set => Value=(int)value; }
    }
    [AddINotifyPropertyChangedInterface]
    public class ViewModel_HashCode
    {
        public ObservableCollection<IntChars> values
        {
            get => new ObservableCollection<IntChars>(string1.Select(x => new IntChars(x)).ToList());
            set { string1=value.ToString(); }
        }

        public ViewModel_HashCode() { }
        public string string1 { private get; set; } = default;
        public int stringHash => string1.GetHashCode();

        public long long1 { private get; set; } = 1;
        public int longHash => long1.GetHashCode();

        public int int1 { private get; set; } = 1;
        public int intHash => int1.GetHashCode();

        public double double1 { private get; set; } = 1;
        public int doubleHash => double1.GetHashCode();

        public float float1 { private get; set; } = 1;
        public int floatHash => float1.GetHashCode();

        public decimal decimal1 { private get; set; } = 1;
        public int decimalHash => decimal1.GetHashCode();

        public bool bool1 { private get; set; } = true;
        public int boolHash => bool1.GetHashCode();

        public char char1 { private get; set; } = 'a';
        public int charHash => char1.GetHashCode();



    }
}
