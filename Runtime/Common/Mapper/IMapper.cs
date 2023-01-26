namespace Northgard.Interactor.Common.Mapper
{
    internal interface IMapper<TS,TT>
    {
        public TT MapToTarget(TS source);
        public TS MapToSource(TT target);
    }
}