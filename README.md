```mermaid
erDiagram
  Employee }|--|{ Contract : h
  Client ||--|{ Contract : h
  Contract ||--|{ WorkPlan : h
  Employee }|--|| Position : h
  
  Employee{
      int Id
      string Name
      string Surname
      int PositionId
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
      string Address
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
  
  Position{
      int Id
      string Name
  }
  ```
