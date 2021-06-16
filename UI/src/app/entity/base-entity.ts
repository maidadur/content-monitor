import { Guid } from "guid-typescript";

export class BaseEntity {
  public id: string;
  public createdOn: string;

  constructor(entity) {
    for (let columnName in entity) {
      this[columnName] = entity[columnName];
    }
  }
}
