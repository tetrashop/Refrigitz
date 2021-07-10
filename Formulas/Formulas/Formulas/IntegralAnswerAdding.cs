namespace Formulas
{
    internal static class IntegralAnswerAdding
    {
        private static AddToTreeTreeLinkList Answer = new AddToTreeTreeLinkList();
        public static void IntegralAnswerAddingFx(AddToTree.Tree Dummy)
        {
            IntegralAnswerAdding.IntegralAnswerAddingActionFx(Dummy);
        }

        private static void IntegralAnswerAddingActionFx(AddToTree.Tree Dummy)
        {
            Answer.CreateLinkListFromTree1(Dummy);
        }
        public static AddToTreeTreeLinkList AnswerAccess
        {
            get
            { return Answer; }
            set { Answer = value; }

        }
    }
}
