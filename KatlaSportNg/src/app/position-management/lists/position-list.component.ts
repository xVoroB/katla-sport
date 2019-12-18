import { Component, OnInit } from '@angular/core';
import { PositionListItem } from '../models/position-list-item';
import { PositionService } from '../services/position.service';

@Component({
  selector: 'app-position-list',
  templateUrl: './position-list.component.html',
  styleUrls: ['./position-list.component.css']
})
export class PositionListComponent implements OnInit {

  positions: PositionListItem[];
  fileToUpload: File = null;

  constructor(private positionService: PositionService) { }

  ngOnInit() {
    this.getPositions();
  }

  getPositions() {
    this.positionService.getPositions().subscribe(h => this.positions = h);
  }

  onDelete(positionId: number) {
    var position = this.positions.find(h => h.id == positionId);
    this.positionService.deletePosition(positionId).subscribe(c => this.getPositions());
  }

}
