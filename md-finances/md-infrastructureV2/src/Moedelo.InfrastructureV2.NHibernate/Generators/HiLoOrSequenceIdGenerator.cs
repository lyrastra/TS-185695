using System;
using System.Collections.Generic;
using System.Data;
using NHibernate.Dialect;
using NHibernate.Engine;
using NHibernate.Id;
using NHibernate.Type;
using NHibernate.Util;

namespace Moedelo.InfrastructureV2.NHibernate.Generators
{
    public class HiLoOrSequenceIdGenerator : TableHiLoGenerator
    {
        private string WhereClause { get; set; }

        public override void Configure(IType type, IDictionary<string, string> parms, Dialect dialect)
        {
            WhereClause = PropertiesHelper.GetString("where", parms, "");

            base.Configure(type, parms, dialect);
        }

        public override object Generate(ISessionImplementor session, object obj)
        {
            using (var connection = session.ConnectionManager.Factory.ConnectionProvider.GetConnection())
            {
                switch (WhereClause)
                {
                    case "TableName='Salary_Worker'":
                        return GetId(connection, "dbo.Salary_WorkerId_Sequence", "int") ??
                               base.Generate(session, obj);

                    case "TableName='Salary_WorkerNdflStatusHistory'":
                        return GetId(connection, "dbo.Salary_WorkerNdflStatusHistoryId_Sequence", "bigint") ??
                               base.Generate(session, obj);

                    case "TableName='Salary_Division'":
                        return GetId(connection, "dbo.Salary_DivisionId_Sequence", "int") ??
                               base.Generate(session, obj);

                    case "TableName='Salary_WorkerPositionType'":
                        return GetId(connection, "dbo.Salary_WorkerPositionTypeId_Sequence", "int") ??
                               base.Generate(session, obj);

                    case "TableName='Salary_WorkerPosition'":
                        return GetId(connection, "dbo.Salary_WorkerPositionId_Sequence", "int") ??
                               base.Generate(session, obj);

                    case "TableName='Salary_WorkerPositionHistory'":
                        return GetId(connection, "dbo.Salary_WorkerPositionHistoryId_Sequence", "int") ??
                               base.Generate(session, obj);

                    case "TableName='Salary_SpecialSchedule'":
                        return GetId(connection, "dbo.Salary_SpecialScheduleId_Sequence", "bigint") ??
                               base.Generate(session, obj);

                    case "TableName='Salary_Foreigner'":
                        return GetId(connection, "dbo.Salary_ForeignerId_Sequence", "bigint") ??
                               base.Generate(session, obj);

                    case "TableName='Salary_ScheduleElement'":
                        return GetId(connection, "dbo.Salary_ScheduleElementId_Sequence", "bigint") ??
                               base.Generate(session, obj);

                    case "TableName='Salary_ExpensesAccountSetting'":
                        return GetId(connection, "dbo.Salary_ExpensesAccountSettingId_Sequence", "bigint") ??
                               base.Generate(session, obj);

                    case "TableName='Salary_TaxationSystemSetting'":
                        return GetId(connection, "dbo.Salary_TaxationSystemSettingId_Sequence", "bigint") ??
                               base.Generate(session, obj);
                        
                    case "TableName='Salary_BusinessTripExpense'":
                        return GetId(connection, "dbo.Salary_BusinessTripExpenseId_Sequence", "bigint") ??
                               base.Generate(session, obj);
                        
                    case "TableName='Salary_ImportFrom1CStatus'":
                        return GetId(connection, "dbo.Salary_ImportFrom1CStatusId_Sequence", "bigint") ??
                               base.Generate(session, obj);
                        
                    case "TableName='Salary_BusinessTrip'":
                        return GetId(connection, "dbo.Salary_BusinessTripId_Sequence", "bigint") ??
                               base.Generate(session, obj);
                        
                    case "TableName='Salary_FiredSalaryInfo'":
                        return GetId(connection, "dbo.Salary_FiredSalaryInfoId_Sequence", "bigint") ??
                               base.Generate(session, obj);
                        
                    case "TableName='Salary_ChartOfAccountsChargeTypeRelation'":
                        return GetId(connection, "dbo.Salary_ChartOfAccountsChargeTypeRelationId_Sequence", "bigint") ??
                               base.Generate(session, obj);
                        
                    case "TableName='Salary_ChartOfAccounts'":
                        return GetId(connection, "dbo.Salary_ChartOfAccountsId_Sequence", "bigint") ??
                               base.Generate(session, obj);
                        
                    case "TableName='Salary_CalculationPeriod'":
                        return GetId(connection, "dbo.Salary_CalculationPeriodId_Sequence", "bigint") ??
                               base.Generate(session, obj);
                        
                    case "TableName='Salary_ResidueType'":
                        return GetId(connection, "dbo.Salary_ResidueTypeId_Sequence", "int") ??
                               base.Generate(session, obj);
                        
                    case "TableName='Salary_FiredWorker'":
                        return GetId(connection, "dbo.Salary_FiredWorkerId_Sequence", "bigint") ??
                               base.Generate(session, obj);
                        
                    case "TableName='Salary_ChargeType'":
                        return GetId(connection, "dbo.Salary_ChargeTypeId_Sequence", "int") ??
                               base.Generate(session, obj);
                        
                    case "TableName='Salary_ExecutedPeriod'":
                        return GetId(connection, "dbo.Salary_ExecutedPeriodId_Sequence", "bigint") ??
                               base.Generate(session, obj);
                        
                    case "TableName='Salary_FirmSettings'":
                        return GetId(connection, "dbo.Salary_FirmSettingsId_Sequence", "bigint") ??
                               base.Generate(session, obj);
                        
                    case "TableName='Salary_ChargeParentType'":
                        return GetId(connection, "dbo.Salary_ChargeParentTypeId_Sequence", "int") ??
                               base.Generate(session, obj);
                        
                    case "TableName='Salary_BusinessTripDailyAllowance'":
                        return GetId(connection, "dbo.Salary_BusinessTripDailyAllowanceId_Sequence", "bigint") ??
                               base.Generate(session, obj);
                        
                    case "TableName='Salary_SumMrot'":
                        return GetId(connection, "dbo.Salary_SumMrotId_Sequence", "int") ??
                               base.Generate(session, obj);
                        
                    case "TableName='Salary_ReportEvent'":
                        return GetId(connection, "dbo.Salary_ReportEventId_Sequence", "bigint") ??
                               base.Generate(session, obj);
                        
                    case "TableName='Salary_ReportWizard'":
                        return GetId(connection, "dbo.Salary_ReportWizardId_Sequence", "bigint") ??
                               base.Generate(session, obj);
                        
                    case "TableName='Salary_WorkerCardAccount'":
                        return GetId(connection, "dbo.Salary_WorkerCardAccountId_Sequence", "int") ??
                               base.Generate(session, obj);
                        
                    case "TableName='Salary_Residue'":
                        return GetId(connection, "dbo.Salary_ResidueId_Sequence", "int") ??
                               base.Generate(session, obj);
                        
                    case "TableName='Salary_Report'":
                        return GetId(connection, "dbo.Salary_ReportId_Sequence", "bigint") ??
                               base.Generate(session, obj);
                        
                    case "TableName='Salary_SickList'":
                        return GetId(connection, "dbo.Salary_SickListId_Sequence", "int") ??
                               base.Generate(session, obj);
                    
                    case "TableName='Salary_Vacation'":
                        return GetId(connection, "dbo.Salary_VacationId_Sequence", "bigint") ??
                               base.Generate(session, obj);
                    
                    case "TableName='Salary_WorkerSalarySetting'":
                        return GetId(connection, "dbo.Salary_WorkerSalarySettingId_Sequence", "bigint") ??
                               base.Generate(session, obj);
                    
                    case "TableName='Salary_ReportXml'":
                        return GetId(connection, "dbo.Salary_ReportXmlId_Sequence", "bigint") ??
                               base.Generate(session, obj);
                    
                    case "TableName='Salary_ChildCare'":
                        return GetId(connection, "dbo.Salary_ChildCareId_Sequence", "bigint") ??
                               base.Generate(session, obj);

                    default:
                        return base.Generate(session, obj);
                }
            }
        }

        private object GetId(IDbConnection connection, string sequence, string sqlType)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                
                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                        $"if object_id('{sequence}', 'SO') is not null " +
                        "begin " +
                        $"declare @id {sqlType}; set @Id = next value for {sequence}; select @id " +
                        "end " +
                        "else " +
                        "select null";
                    
                    var result = command.ExecuteScalar();
                    
                    return result == DBNull.Value ? null : result;
                }
            }
            catch (Exception e)
            {
                throw new Exception($"HiLoOrSequenceIdGenerator GetId error. ConnectionString - {connection.ConnectionString}.", e);
            }
        }
    }
}
