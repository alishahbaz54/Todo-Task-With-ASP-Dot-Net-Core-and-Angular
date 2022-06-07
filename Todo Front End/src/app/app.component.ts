import { Component } from '@angular/core';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent
{
  // title = 'DragDropApp';
  Movies = [
    'Blade Runner',
    'Cool Hand Luke',
    'Heat',
    'Juice',
    'The Far Side of the World',
    'Morituri',
    'Napoleon Dynamite',
    'Pulp Fiction'
  ];
  MoviesList = [
    'The Far Side of the World',
    'Morituri',
    'Napoleon Dynamite',
    'Pulp Fiction',
    'Blade Runner',
    'Cool Hand Luke',
    'Heat',
    'Juice'
  ];
  MoviesWatched = [
  ];
  drop(event: CdkDragDrop<string[]>)
  {
    moveItemInArray(this.Movies, event.previousIndex, event.currentIndex);
  }
  onDrop(event: CdkDragDrop<string[]>)
  {
    if (event.previousContainer === event.container)
    {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else
    {
      transferArrayItem(event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex);
    }
  }
}