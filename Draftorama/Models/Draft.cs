using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draftorama.Models
{
    public class Draft
    {
        #region Public Fields

        public const int TableSize = 8;

        #endregion Public Fields

        #region Public Constructors

        public Draft(Set draftSetIn)
        {
            SetBeingDrafted = draftSetIn;
            SetName = SetBeingDrafted.SetName;
            Random r = new Random();
            DraftName = r.Next(100000, 999999).ToString();
            DraftDate = DateTime.Now.Date;
            Table = GeneratePlayers();
            PacksInDraft = new List<Pack>();
            OpenPacks();
        }

        #endregion Public Constructors

        #region Public Properties

        public DateTime DraftDate { get; set; }
        public string DraftName { get; set; }
        public Set SetBeingDrafted { get; private set; }
        public string SetName { get; set; }
        public List<Player> Table { get; private set; }
        public string UserName { get; set; }

        #endregion Public Properties

        #region Private Properties

        private List<Pack> PacksInDraft { get; set; }

        #endregion Private Properties

        #region Public Methods

        public List<Player> GeneratePlayers()
        {
            List<Player> playahs = new List<Player>(TableSize);
            for (int p = 0; p < playahs.Capacity; p++)
            {
                playahs.Add(new Player());
            }
            return playahs;
        }

        public void OpenPacks()
        {
            foreach (Player player in Table)
            {
                Pack p = new Pack(SetBeingDrafted);
                player.PackInHand = p;
                PacksInDraft.Add(p);
            }
        }

        public void TriggerPick()
        {
            foreach (Player player in Table)
            {
                player.MakePick();
            }
            if (PacksInDraft.Sum<Pack>(a => a.NumCardsInPack) > 0)
            {
                Pack passedPack = Table.Last().PackInHand;
                foreach (Player player in Table)
                {
                    int seat = Table.FindIndex(a => a == player);
                    if (seat < TableSize)
                    {
                        Pack tempPack = Table.ElementAt(seat).PackInHand;
                        Table.ElementAt(seat).PackInHand = passedPack;
                        passedPack = tempPack;
                    }
                }
            }
            else
            {
                OpenPacks();
            }
        }

        #endregion Public Methods
    }
}