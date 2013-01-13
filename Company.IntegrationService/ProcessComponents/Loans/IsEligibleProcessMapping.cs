using Company.IntegrationService.Contracts.DataContracts;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.LOB.LoanManagement.Entities;

namespace Company.IntegrationService.ProcessComponents.Loans
{
    public class IsEligibleProcessMappings
    {
        //public ApplicantToCustomerAccountMap ApplicantToCustomerAccountMap = new ApplicantToCustomerAccountMap();
        //public BooleanToIsEligibleResponseMap BooleanToIsEligibleResponseMap = new BooleanToIsEligibleResponseMap();

        /// <summary>
        /// Maps from an <see cref="Applicant"/> data contract to a
        /// <see cref="CustomerAccount"/> Loan Management entity.
        /// </summary>
        /// <param name="applicant">An <see cref="Applicant"/> data contract to map from</param>
        /// <returns>The <see cref="CustomerAccount"/> Loan Management entity to return</returns>
        public CustomerAccount MapFromApplicantToCustomerAccount(Applicant applicant)
        {
            return new CustomerAccount { Name = applicant.Name };
        }


        /// <summary>
        /// Creates an <see cref="IsEligibleResponse"/> data contract from the boolean response
        /// that is returned from the Loan Management system.
        /// </summary>
        /// <param name="isEligible">True if the <see cref="Applicant"/> is eligible for a loan</param>
        /// <returns>The <see cref="IsEligibleResponse"/> data contract type to return.</returns>
        public IsEligibleResponse MapFromBoolToIsEligibleResponse(bool isEligible)
        {
            return new IsEligibleResponse { Eligible = isEligible };
        }
    }
}
