```mermaid
erDiagram
  Employee }|--|{ Contract : ""
  Client ||--|{ Contract : ""
  Contract ||--|{ WorkPlan : ""
  Employee }|--|| Position : ""
  
  Employee{
      int Id
      string Name
      int PositionId
      int ContractId
  }

  Client{
      int Id
      string Name
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
      DateTime DateStart
      DateTime DateFinish
      int ContractId
  }
  
  Position{
      int Id
      string Name
  }
  ```
