namespace TravisRFrench.UI.MVVM.Samples.Common
{
    public interface ITransaction<in TTransactee>
    {
        void Execute(TTransactee transactee);
    }
}
