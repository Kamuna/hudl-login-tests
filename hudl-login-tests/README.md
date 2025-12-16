# Hudl Login Tests

Selenium-based login tests for Hudl using LightBDD and xUnit.

## Setup

Add your credentials to `testSettings.json`:
```json
{
  "Browser": {
    "Headless": true,
    "Type": "chrome"
  },
  "Hudl": {
    "HomepageUrl": "https://www.hudl.com/en_gb/",
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

## Browser Options

Environment variables: `BROWSER_TYPE`, `BROWSER_HEADLESS`
