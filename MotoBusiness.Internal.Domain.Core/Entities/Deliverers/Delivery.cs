using MotoBusiness.Internal.Domain.Core.Abstractions;
using MotoBusiness.Internal.Domain.Core.Entities.Rentals;

namespace MotoBusiness.Internal.Domain.Core.Entities.Deliverers
{
    public sealed class Delivery : BaseEntity
    {
        protected Delivery()
        {
            Rentals = new();
        }

        public Delivery(string name, DateTime birthDate, string cnpj, CNH cnh)
        {
            ChangeCNH(cnh);
            ChangeName(name);
            ChangeCNPJ(cnpj);
            ChangeBirthDate(birthDate);
            Rentals = new();
        }

        public int DeliveryId { get; private set; }
        public string Name { get; set; } = default!;
        public DateTime BirthDate { get; set; }
        public string CNPJ { get; set; } = default!;
        public CNH CNH { get; private set; } = default!;

        public List<Rental> Rentals { get; private set; }

        #region - Business Rules -

        public void ChangeCNH(CNH cnh)
        {
            var errors = cnh.Validate();
            if (errors.Count > 0)
            {
                AddErrors(errors);
                return;
            }

            CNH = cnh;
        }

        /// <summary>
        /// Regras de negocio referente ao CNPJ
        /// Todo: Implementar verificação de check digit
        /// </summary>
        /// <param name="cnpj"></param>
        public void ChangeCNPJ(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj))
            {
                var _error = DeliveryErrors.CNPJ_Invalid;

                AddError(_error.Description!);

                return;
            }

            CNPJ = cnpj;
        }

        /// <summary>
        /// Regras de negocio referente ao nome do entregador.
        /// Todo: Implementar as demais regras se aplicaveis.
        /// Exemplo: Verificar se possui o primeiro e ultimo nome
        /// </summary>
        /// <param name="name"></param>
        public void ChangeName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var _error = DeliveryErrors.NameInvalid;

                AddError(_error.Description!);

                return;
            }

            Name = name;
        }

        /// <summary>
        /// Regra de negocio referente a data de nascimento.
        /// Todo: Implementar todas as regras aplicaveis.
        /// </summary>
        /// <param name="birthdate">Data de Nasciimento</param>
        public void ChangeBirthDate(DateTime birthdate)
        {
            var age = DateTime.Now.Year - birthdate.Year;

            if (age < 18)
            {
                var _error = DeliveryErrors.BirthDateInvalid;

                AddError(_error.Description!);

                return;
            }

            BirthDate = birthdate;
        }

        public bool CheckSuitableForAllocation()
        {
            return CNH.Type == CNHType.A || CNH.Type == CNHType.AB;
        }

        public void AddRental(Rental rental)
        {
            Rentals.Add(rental);
        }

        #endregion
    }
}