namespace Kkmlsfi_ns.API.Models.Domain
{
    public partial class Homecell
    {
        public string PraiseAndWorshipMembers
        {
            get
            {
                string result = "";
                if (HomecellPraiseAndWorshipMembers != null)
                {
                    foreach (var pnw in HomecellPraiseAndWorshipMembers)
                    {
                        if (result == "")
                        {
                            result += pnw.Member.FullName;
                        }
                        else
                        {
                            result += $", {pnw.Member.FullName}";
                        }
                    }
                }

                return result;
            }
        }

        public string PraiseAndWorshipMemberIds
        {
            get
            {
                string result = "";
                if (HomecellPraiseAndWorshipMembers != null)
                {
                    foreach (var pnw in HomecellPraiseAndWorshipMembers)
                    {
                        if (result == "")
                        {
                            result += pnw.MemberId;
                        }
                        else
                        {
                            result += $", {pnw.MemberId}";
                        }
                    }
                }

                return result;
            }
        }
    }
}
