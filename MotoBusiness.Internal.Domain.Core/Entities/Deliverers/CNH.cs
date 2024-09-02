namespace MotoBusiness.Internal.Domain.Core.Entities.Deliverers
{
    public sealed class CNH
    {
        private readonly List<string> _errors = new();

        public CNH(string number, CNHType type, string? url = null)
        {
            ChangeNumber(number);
            ChangeType(type);
            ChangeUrl(url);
        }

        public string Number { get; private set; } = default!;
        public CNHType Type { get; private set; }
        public string? ImageUrl { get; private set; }

        public List<string> Validate() => _errors;

        /// <summary>
        /// Regras de negocio referente ao numero de CNH
        /// Todo: Aplicar validações sobre o CNH inclusive Check Digit.
        /// </summary>
        /// <param name="number"></param>
        public void ChangeNumber(string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                var error = DeliveryErrors.CNH_NumberInvalid;

                _errors.Add(error.Description!);

                return;
            }

            Number = number;
        }

        public void ChangeType(CNHType type)
        {
            Type = type;
        }

        public void ChangeUrl(string? url)
        {
            if (string.IsNullOrEmpty(url))
            {
                var error = DeliveryErrors.CNH_ImageInvalid;

                _errors.Add(error.Description!);

                return;
            }

            ImageUrl = url;
        }
    }
}

