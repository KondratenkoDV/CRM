```mermaid
erDiagram
  Employee }|--|{ Contract : ""
  Client ||--|{ Contract : ""
  Contract ||--|{ WorkPlan : ""
  Employee }|--|| Position : ""
  
  Employee{
      int Id
      string Name "[Unique]"
      int PositionId
      int ContractId
  }

  Client{
      int Id
      string Name "[Unique]"
      int PhonNumber
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
      DateTime DateStart "[Unique]"
      DateTime DateFinish "[Unique]"
      int ContractId
  }
  
  Position{
      int Id
      string Name "[Unique]"
  }
  ```
