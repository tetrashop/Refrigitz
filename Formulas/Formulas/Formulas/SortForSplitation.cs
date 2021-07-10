namespace Formulas
{
    internal static class SortForSplitation
    {
        public static AddToTree.Tree SortForSplitationFx(AddToTree.Tree Dummy)
        {
            if (Dummy.SampleAccess == "/")
            {
                Dummy = SortForSplitation.SortForSplitationActionSenderFx(Dummy.LeftSideAccess, Dummy.RightSideAccess);
            }

            return Dummy;
        }

        private static AddToTree.Tree SortForSplitationActionSenderFx(AddToTree.Tree DLeft, AddToTree.Tree DRight)
        {
            return DLeft;
        }

        private static AddToTree.Tree SortForSplitationActionReciverFx(AddToTree.Tree Dummy, AddToTree.Tree DummySender)
        {
            return Dummy;
        }
    }

}
