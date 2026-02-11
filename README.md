# Expense Manager - Lab 1

A C# console application for tracking wallets and transactions, built with **Multilayer Architecture** and **SOLID principles**.

## Project Structure
The solution is divided into 4 projects to ensure **Single Responsibility (SRP)**:

1.  **ExpenseManager.Models**: Contains "clean" data entities (Wallet, Transaction) and Enums.
    * *Constraint:* No calculated fields or collection properties here.
2.  **ExpenseManager.ViewModels**: Handles data presentation and UI logic.
    * *Logic:* Contains calculated properties like `TotalBalance` and `IsExpense`.
3.  **ExpenseManager.Data**: Acts as the Data Access Layer (DAL).
    * *Storage:* Includes an `internal` fake storage with pre-defined data (12 transactions).
    * *Service:* Contains `ExpenseService` for filtering and mapping models.
4.  **ExpenseManager.ConsoleApp**: The entry point. Handles user input and navigation.

## Key Architecture Features
- **Decoupled Entities**: Wallet models do not contain lists of transactions. Connections are handled via `WalletId` in the Service layer.
- **Calculated Logic**: All business logic (e.g., balance summation) is encapsulated in ViewModels, keeping Data Models "pure".
- **Encapsulation**: Storage is `internal`, meaning it is protected from direct access by the UI layer.

## How to Run
1. Open `ExpenseManager.sln` in JetBrains Rider or Visual Studio.
2. Set `ExpenseManager.ConsoleApp` as the Startup Project.
3. Run (F5).