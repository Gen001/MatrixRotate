import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { FormControl } from '@angular/forms';
import { MatrixService } from '../shared/services/matrix.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  rowColumns: string[] = ['rowId'];
  displayedColumns: string[];
  matrixColumns: string[] = [];
  dataSource: MatTableDataSource<number[]>;
  matrixSize = new FormControl('1');

  constructor(private matrixService: MatrixService) {
  }
  ngOnInit(): void {
    this.dataSource = new MatTableDataSource();
  }

  formatColumns(data) {
    this.dataSource = new MatTableDataSource(data);
    this.matrixColumns = [];
    this.displayedColumns = this.rowColumns.slice();
    for (let index = 0; index < data.length; index++) {
      this.matrixColumns.push(index.toString());
      this.displayedColumns.push(index.toString());

    }
  }
  generate() {
    const size = +this.matrixSize.value;
    this.matrixService.generate(size).subscribe(res => {
      this.matrixService.getByRow(0, size).subscribe(data => {
        this.formatColumns(data);
      });
    });
  }

  import() {

  }

  export() {

  }

  rotate() {
    const size = +this.matrixSize.value;
    this.matrixService.rotate().subscribe(res => {
      this.matrixService.getByRow(0, size).subscribe(data => {
        this.formatColumns(data);
      });
  });

  }
}
