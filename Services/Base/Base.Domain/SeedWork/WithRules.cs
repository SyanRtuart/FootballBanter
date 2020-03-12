namespace Base.Domain.SeedWork
{
    public abstract class WithRules
    {
        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken()) throw new BusinessRuleValidationException(rule);
        }
    }
}