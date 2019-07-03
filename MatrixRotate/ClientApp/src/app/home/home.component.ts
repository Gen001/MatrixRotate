import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { FormControl } from '@angular/forms';
import { saveAs } from 'file-saver';
import { MatrixService } from '../shared/services/matrix.service';
import { mergeMap } from 'rxjs/operators';

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
  fileCsv = new FormControl('');
  size = 1;

  constructor(private matrixService: MatrixService) {
  }
  ngOnInit(): void {
    this.matrixService.getSize().pipe(
      mergeMap(size => {
        this.size = size;
        return this.matrixService.getByRow(0, size);
      })
    ).subscribe(data => this.formatColumns(data));
  }

  formatColumns(data: number[][]) {
    this.matrixSize.setValue(data.length);
    this.dataSource = new MatTableDataSource(data);
    this.matrixColumns = [];
    this.displayedColumns = this.rowColumns.slice();
    for (let index = 0; index < data.length; index++) {
      this.matrixColumns.push(index.toString());
      this.displayedColumns.push(index.toString());

    }
  }
  generate() {
    this.size = +this.matrixSize.value;
    this.matrixService.generate(this.size).pipe(
      mergeMap(() => this.matrixService.getByRow(0, this.size)))
      .subscribe(data => this.formatColumns(data));
  }

  onFileSelect(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.import(file);
    }
  }

  import(file) {
    const formData = new FormData();
    formData.append('file', file);
    this.matrixService.import(formData).pipe(
      mergeMap(size => this.matrixService.getByRow(0, size)))
      .subscribe(data => {
        this.formatColumns(data);
        this.fileCsv.setValue(null);
      });
  }

  export() {
    this.matrixService.export().subscribe(res => {
      const data = new Blob([res], { type: 'text/plain;charset=utf-8' });
      const filename = 'Matrix.csv';
      saveAs(data, filename);
    });
  }
  rotate() {
    this.matrixService.rotate().pipe(
      mergeMap(() => this.matrixService.getByRow(0, this.size))
    ).subscribe(data => this.formatColumns(data));
  }
}
