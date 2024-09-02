namespace MotoBusiness.Internal.Domain.Core.Abstractions
{
    public abstract class BaseEntity
	{
        private readonly List<string> _errors;
        public BaseEntity()
        {
            _errors = new List<string>();
        }

        public IReadOnlyCollection<string> Errors { get => _errors.ToList(); }
        public bool IsValid { get => Errors.Count == 0; }

        public virtual void AddError(string message)
        {
            if (string.IsNullOrEmpty(message))
                _errors.Add(message);
        }

        public virtual void AddErrors(List<string> errors)
        {
            foreach (var error in errors) AddError(error);
        }
    }
}