```mermaid
erDiagram
  Employee }|--|{ Contract
  Client ||--|{ Contract
  Contract ||--|{ WorkPlan
  Employee{
      int Id
      string Name
      string Surname
      string Position
      int ContractId
  }

  Client{
      int Id
      string Name
      int ContactPhonNumber
  }

  Contract{
      int Id
      string Subject
      decimal Price
      int ClientId
      int EmployeeId
  }

  WorkPlan{
      int Id
      DataTime DataStart
      DataTime DataFinish
      int ContractId
  }
  ```
