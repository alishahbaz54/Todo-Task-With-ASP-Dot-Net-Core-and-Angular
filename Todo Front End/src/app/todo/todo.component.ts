import { Component, Input, OnInit } from '@angular/core';
import { Color } from '../Models/color.model';
import { Todo } from '../Models/todo.moddel';
import { TodoService } from '../Services/todo.service';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop'

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.css']
})
export class TodoComponent implements OnInit
{

  @Input() todo: Todo = new Todo;
  @Input() colorModel: Color = new Color;


  todoList: any;

  colorBox: any = null;
  // inVisibleField: boolean = false;
  disableField = true;

  holdTodoList: any;
  doneProp: any = false;
  colorBoxProp: any;
  // hideTextBoxProp: any = null;
  hideInnerTextBtn: any = true;
  propForPTag: any = false;
  delDefaultProp: any = true;
  delPropVal: any;
  todoId: any;


  constructor(private todoService: TodoService) { }

  ngOnInit(): void
  {
    this.ResetModel();
    this.GetAllTodo();
    this.ResetColorModel();
    // this.LoadAllTodo();
  }
  ResetColorModel()
  {
    try
    {
      this.colorModel = {
        ColorCode: null
      };
    }
    catch (ex)
    {
      alert(ex);
    }
  }
  colorCode: any;
  hCode = '#712828';

  showVal = "";
  ShowTextBox(id: any)
  {
    let currentId = this.holdTodoList.find((i: any) => i.id == id);

    if (currentId != null && currentId.isDone != true)
    {
      if (this.hideInnerTextBtn == true)
      {
        this.propForPTag = true;
        this.colorBox = currentId.id;
        this.hideInnerTextBtn = false;
        this.disableField = false;
      }
      else
      {
        this.hideInnerTextBtn = true;
      }
    }
  }
  GetAllTodo()
  {
    try
    {
      this.todoService.GetTodos().subscribe({
        next: (data) =>
        {
          this.todoList = data;
          this.holdTodoList = this.todoList
          for (let d = 0; d < this.todoList.length - 1; d++)
          {
            if (this.todoList[d].isDone == false)
            {
              this.doneProp = false;
            } else
            {
              this.doneProp = true;
            }
          }
        }, error: (e) => console.log(e)
      });
    }
    catch (ex)
    {
      alert(ex);
    }
  }
  ShowColorBox(id: any)
  {
    let currentId = this.holdTodoList.find((i: any) => i.id == id);
    if (currentId != null)
    {
      this.colorBoxProp = currentId.id;

    }
  }
  GetColor(id: any)
  {
    if (this.colorModel.ColorCode != null && id != null)
    {
      let colorData = {
        colorCode: this.colorModel.ColorCode
      };
      this.todoService.SetColorApi(this.holdTodoList.colorId).subscribe((data: {}) =>
      {

      });
    }
    this.GetAllTodo();
    this.ResetColorModel();
    this.ResetColorModel();
  }
  ResetModel()
  {
    try
    {
      this.todo = {
        Id: null,
        Description: null,
        IsDone: null,
        ColorId: null,
        Position: null
      };
    }
    catch (ex)
    {
      console.log("Reset model error");
    }
  }
  AddData()
  {
    try
    {
      if (this.todo.Description != null && this.todo.Description != "")
      {
        this.todo.IsDone = false;
        this.todo.Id = 0;
        this.todo.ColorId = 12;
        this.todoService.CreateTodo(this.todo).subscribe((data: {}) =>
        {
          // alert("Added");
          this.ResetModel();
          this.GetAllTodo();
        });

      }
      else
      {
        alert("Required");
      }
    }
    catch (ex)
    {
      console.log("Inserting data error");
    }
  }
  DeleteFunction(_id: any)
  {
    if (_id != null)
    {
      this.todoService.Delete(_id).subscribe((data) =>
      {
        this.GetAllTodo();
      });
    }
  }
  UpdateTodoItem(_id: any, _data: any)
  {
    try
    {
      let todoDoneId = this.holdTodoList.find((i: any) => i.id == _id);
      if (todoDoneId.id != null && _data.trim() != "")
      {
        this.todo = {
          Id: _id,
          Description: _data,
          IsDone: todoDoneId.isDone,
          ColorId: todoDoneId.colorId,
          Position: todoDoneId.Postion
        };
        this.todoService.UpdateTodoApi(_id, this.todo).subscribe((data: {}) =>
        {
          this.ResetModel();
          this.GetAllTodo();
          this.colorBox = null;
        }, err =>
        {
          alert(err.message);
        });
      }
      else
      {
        alert("Doesn't allow empity todo item");
      }
    } catch (ex)
    {
      alert(ex);
    }
  }

  MarkTodoDone(_id: any)
  {
    let todoDoneId = this.holdTodoList.find((i: any) => i.id == _id);
    if (todoDoneId != null)
    {
      if (todoDoneId.isDone != true)
      {
        this.todoService.DoneTodo(todoDoneId.id, todoDoneId).subscribe((data: {}) =>
        {
          this.GetAllTodo();
        });
      }
    }
  }

  CloseUpdateOperation(_id: any)
  {
    try
    {
      let todoDoneId = this.holdTodoList.find((i: any) => i.id == _id);
      if (_id != null)
      {
        this.propForPTag = false;
        this.colorBox = null;
      }
    }
    catch (ex)
    {
      alert(ex);
    }
  }

  AssignColor(id: any)
  {
    try
    {
      this.todoService.GetColorApi(id).subscribe({
        next: (data) =>
        {
          this.colorCode = data;
        }
      })
    }
    catch (ex)
    {
      alert(ex);
    }
  }
  HideColorBox(_id: any)
  {
    this.colorBoxProp = null;
    let colorId = this.holdTodoList.find((i: any) => i.id == _id);
    if (this.colorModel.ColorCode != null)
    {
      this.colorModel = {
        ColorCode: this.colorModel.ColorCode
      };
      this.todoService.PutTodoCoolorApi(_id, this.colorModel).subscribe((data: {}) =>
      {
        this.ResetColorModel();
        this.ResetModel();
        this.GetAllTodo();
      });
    }
  }

  // delClickPorop
  DeleteShowFun(_id: any)
  {
    if (_id != null)
    {
      if (_id != null)
      {
        if (this.delDefaultProp == true)
        {
          this.delDefaultProp = false;
          this.delPropVal = _id;
        }
        else
        {
          this.delDefaultProp = true;
        }
      }
    }
  }

  drop(event: CdkDragDrop<Todo[]>)
  {
    moveItemInArray(this.todoList, event.previousIndex, event.currentIndex);
    event.container = event.previousContainer;
    this.todoId = this.todoList[event.currentIndex];
    this.todo = {
      Id: this.todoId.id,
      Description: this.todoId.description,
      IsDone: this.todoId.isDone,
      ColorId: this.todoId.colorId,
      Position: event.currentIndex
    };
    this.SetPosition(this.todoId.id, this.todo);
    this.ResetModel();
  }
  SetPosition(id: any, data: any)
  {
    try
    {
      // console.log(data);
      this.todoService.SetPositionApi(id, data).subscribe((data: {}) =>
      {
        this.GetAllTodo();
      });
    }
    catch (ex)
    {
      alert(ex);
    }
  }
}
