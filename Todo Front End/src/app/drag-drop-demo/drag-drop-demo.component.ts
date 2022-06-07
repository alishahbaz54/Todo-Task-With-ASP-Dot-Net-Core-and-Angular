import { Component, OnInit } from '@angular/core';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';


@Component({
  selector: 'app-drag-drop-demo',
  templateUrl: './drag-drop-demo.component.html',
  styleUrls: ['./drag-drop-demo.component.css']
})
export class DragDropDemoComponent implements OnInit
{

  constructor() { }

  ngOnInit(): void
  {
  }
  movies = [
    'Episode I - The Phantom Menace',
    'Episode II - Attack of the Clones',
    'Episode III - Revenge of the Sith',
    'Episode IV - A New Hope',
    'Episode V - The Empire Strikes Back',
    'Episode VI - Return of the Jedi',
    'Episode VII - The Force Awakens',
    'Episode VIII - The Last Jedi',
    'Episode IX – The Rise of Skywalker',
  ];

  drop(event: CdkDragDrop<string[]>)
  {
    moveItemInArray(this.movies, event.previousIndex, event.currentIndex);
    console.log(event.currentIndex);
  }

}
