using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Draftorama.Models
{
    public class Mana
    {
        #region Private Fields

        private int _black;
        private int _blue;
        private int _colorless;
        private int _generic;
        private int _green;
        private int _identityWeight;
        private int _red;
        private string _relationshipType;
        private string _stringFormat;
        private int _white;

        #endregion Private Fields

        #region Public Constructors

        public Mana(string manaStringIn, string relationTypeIn, int identityWeightIn = 1)
        {
            RelationshipType = CheckRelationString(relationTypeIn);
            StringFormat = manaStringIn;
            IdentityWeight = (int)identityWeightIn;

            ReadFromManaString(manaStringIn);
        }

        public Mana(int genericIn, int whiteIn, int blueIn, int blackIn, int redIn, int greenIn, int colorlessIn, string relationTypeIn)
        {
            _generic = genericIn;
            _white = whiteIn;
            _blue = blueIn;
            _black = blackIn;
            _red = redIn;
            _green = greenIn;
            _colorless = colorlessIn;
            _relationshipType = relationTypeIn;

            _stringFormat = GenerateManaString();
        }

        #endregion Public Constructors

        #region Private Constructors

        private Mana()
        {
        }

        #endregion Private Constructors

        //public enum ManaRelationshipType { CastingCost, AbilityCost, ManaGenerated }

        //public enum ManaTypes { Black, Blue, Colorless, Generic, Green, Hybrid, Red, White }

        #region Public Properties

        public int Black
        {
            get { return _black; }
            set { _black = value; }
        }

        public int Blue
        {
            get { return _blue; }
            set { _blue = value; }
        }

        public int Colorless
        {
            get { return _colorless; }
            set { _colorless = value; }
        }

        public int Generic
        {
            get { return _generic; }
            set { _generic = value; }
        }

        public int Green
        {
            get { return _green; }
            set { _green = value; }
        }

        public int IdentityWeight
        {
            get { return _identityWeight; }
            set { _identityWeight = value; }
        }

        public int Red
        {
            get { return _red; }
            set { _red = value; }
        }

        public string RelationshipType
        {
            get { return _relationshipType; }
            set { _relationshipType = value; }
        }

        public string StringFormat
        {
            get { return _stringFormat; }
            set { _stringFormat = value; }
        }

        public int White
        {
            get { return _white; }
            set { _white = value; }
        }

        #endregion Public Properties

        #region Private Methods

        private string CheckRelationString(string relationIn)
        {
            if (relationIn == "CastingCost" || relationIn == "AbilityCost" || relationIn == "ManaGenerated")
            {
                return relationIn;
            }
            else
            {
                return "Error";
            }
        }

        private string GenerateManaString()
        {
            throw new NotImplementedException();
        }

        private void ReadFromManaString(string manaStringIn)
        {
            if (manaStringIn.First() == 'X')
            {
                Generic = 0;
            }
            else if (manaStringIn.First() != 'W' && manaStringIn.First() != 'U' && manaStringIn.First() != 'B' && manaStringIn.First() != 'R' && manaStringIn.First() != 'G' && manaStringIn.First() != 'C')
            {
                string resultString = Regex.Match(manaStringIn, @"\d+").Value;
                Generic = Int32.Parse(resultString);
            }

            White = manaStringIn.Count(x => x == 'W');
            Blue = manaStringIn.Count(x => x == 'U');
            Black = manaStringIn.Count(x => x == 'B');
            Red = manaStringIn.Count(x => x == 'R');
            Green = manaStringIn.Count(x => x == 'G');
            Colorless = manaStringIn.Count(x => x == 'C');
        }

        #endregion Private Methods
    }
}