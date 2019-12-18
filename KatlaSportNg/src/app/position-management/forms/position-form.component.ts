import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PositionService } from '../services/position.service';
import { Position } from '../models/position';

@Component({
  selector: 'app-position-form',
  templateUrl: './position-form.component.html',
  styleUrls: ['./position-form.component.css']
})
export class PositionFormComponent implements OnInit {

  position = new Position(0, "");
  exists = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private positionService: PositionService
  ) { }

  ngOnInit() {
    this.route.params.subscribe(p => {
      if (p['id'] === undefined) return;
      this.positionService.getPosition(p['id']).subscribe(h => this.position = h);
      this.exists = true;
    });
  }

  navigateToPositions() {
    this.router.navigate(['/positions']);
  }

  onCancel() {
    this.navigateToPositions();
  }

  onSubmit() {
    if (this.exists) {
      this.positionService.updatePosition(this.position).subscribe(c => this.navigateToPositions());
    } else {
      this.positionService.addPosition(this.position).subscribe(c => this.navigateToPositions());
    }

  }
  onDelete() {
    this.positionService.deletePosition(this.position.id).subscribe(p => this.navigateToPositions());
  }
}
