```mermaid
erDiagram
  Employee }|--|{ Contract : ""
  Client ||--|{ Contract : ""
  Contract ||--|{ WorkPlan : ""
  Employee }|--|| Position : ""
  
  Employee{
      int Id
      string Name
      string Surname
      int PositionId
      int ContractId
  }

  Client{
      int Id
      string Name "[Unique]"
      int ContactPhonNumber
  }

  Contract{
      int Id
      string Subject
      string Address
      decimal Price
      int ClientId
      int EmployeeId
  }

  WorkPlan{
      int Id
      DataTime DataStart "[Unique]"
      DataTime DataFinish "[Unique]"
      int ContractId
  }
  
  Position{
      int Id
      string Name "[Unique]"
  }
  ```
