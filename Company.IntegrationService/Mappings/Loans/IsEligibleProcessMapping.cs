using Company.IntegrationService.Contracts.DataContracts;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.LOB.LoanManagement.Entities;

namespace Company.IntegrationService.Mappings.Loans
{
    /// <summary>
    /// Maps from an <see cref="Applicant"/> data contract to a
    /// <see cref="CustomerAccount"/> Loan Management entity.
    /// </summary>
    /// <param name="applicant">An <see cref="Applicant"/> data contract to map from</param>
    /// <returns>The <see cref="CustomerAccount"/> Loan Management entity to return</returns>
    public class IsEligibleInputMapper : MappingFunction<Applicant, CustomerAccount>
    {

        protected override CustomerAccount Default(Applicant input)
        {
            return new CustomerAccount { Name = input.Name };
        }
    }

    /// <summary>
    /// Creates an <see cref="IsEligibleResponse"/> data contract from the boolean response
    /// that is returned from the Loan Management system.
    /// </summary>
    /// <param name="isEligible">True if the <see cref="Applicant"/> is eligible for a loan</param>
    /// <returns>The <see cref="IsEligibleResponse"/> data contract type to return.</returns>
    public class IsEligibleOutputMapper : MappingFunction<bool, IsEligibleResponse>
    {

        protected override IsEligibleResponse Default(bool output)
        {
            return new IsEligibleResponse { Eligible = output };
        }
    }
}
