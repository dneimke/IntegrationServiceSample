using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Company.LOB.LoanManagement.Entities;
using Company.IntegrationService.Contracts.DataContracts;
using Company.IntegrationService.Contracts.MessageContracts;

namespace Company.IntegrationService.ProcessComponents.Loans
{
    public class IsEligibleProcessMappings
    {
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
        public IsEligibleResponse CreateIsEligibleResponse(bool isEligible)
        {
            return new IsEligibleResponse { Eligible = isEligible };
        }
    }
}
