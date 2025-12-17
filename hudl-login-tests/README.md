# Hudl Login Tests

Selenium-based login tests for Hudl using LightBDD and xUnit.

## Setup

Create `testSettings.local.json` with your credentials (gitignored):
```json
{
  "Hudl": {
    "ValidUser": {
      "Email": "your-email@example.com",
      "Password": "your-password"
    }
  }
}
```

## Run Tests

```
dotnet test
```

## Test Data

Add or modify test cases in `testData.json`:
```json
{
  "InvalidEmailTestCases": [...],
  "BoundaryTestCases": [...],
  "LoginTestCases": [...],
  "ExpectedMessages": {...},
  "TestValues": {...},
  "Urls": {...}
}
```

## Test Reports

HTML reports are generated after each test run:
- Location: `TestResults/Reports/FeaturesReport.html`
- Screenshots captured on test failure

## Browser Options

Environment variables: `BROWSER_TYPE`, `BROWSER_HEADLESS`, `BROWSER_SCREENSIZE`

Screen sizes: `desktop` (1920x1080), `laptop` (1366x768), `tablet` (768x1024), `mobile` (375x812)
