(function (webApp) {
    var wizardUrls = {};

    wizardUrls.ClosingPeriodWizard = {};
    wizardUrls.ClosingPeriodWizard.LoadWizard = "/ClosingPeriodWizard/LoadWizard";
    wizardUrls.ClosingPeriodWizard.GetStep = "/ClosingPeriodWizard/GetStep";
    wizardUrls.ClosingPeriodWizard.SaveSelfCostStep = "/ClosingPeriodWizard/SaveExpensesRecognitionStep";
    wizardUrls.ClosingPeriodWizard.SaveAmortizationStep = "/ClosingPeriodWizard/SaveAmortizationStep";
    wizardUrls.ClosingPeriodWizard.SaveFinancialResultStep = "/ClosingPeriodWizard/SaveFinancialResultStep";
    wizardUrls.ClosingPeriodWizard.SaveIncomingWaybillStep = "/ClosingPeriodWizard/SaveIncomingWaybillStep";
    wizardUrls.ClosingPeriodWizard.SavePostingCreationStep = "/ClosingPeriodWizard/SavePostingCreationStep";
    wizardUrls.ClosingPeriodWizard.SaveSalaryPostingStep = "/ClosingPeriodWizard/SaveSalaryPostingStep";
    wizardUrls.ClosingPeriodWizard.SaveNormalizedExpenseStep = "/ClosingPeriodWizard/SaveNormalizedExpenseStep";
    wizardUrls.ClosingPeriodWizard.MoveToNextMonth = "/ClosingPeriodWizard/MoveToAnotherMonth";
    wizardUrls.ClosingPeriodWizard.Cancel = "/ClosingPeriodWizard/Cancel";

    wizardUrls.ClosingPeriodWizard.GetClosedPeriod = "/ClosedPeriods/GetClosedPeriod";
    wizardUrls.ClosingPeriodWizard.OpenPeriod = "/ClosedPeriods/OpenPeriod";

    for (var Controller in wizardUrls) {
        if (typeof wizardUrls[Controller] == "object") {
            if (!WebApp[Controller]) {
                WebApp[Controller] = {};
            }
            
            for (var Action in wizardUrls[Controller]) {
                WebApp[Controller][Action] = WebApp.root + wizardUrls[Controller][Action];
            }
        }
    }
})(window.WebApp || {});