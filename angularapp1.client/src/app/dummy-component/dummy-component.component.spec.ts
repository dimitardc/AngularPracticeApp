import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DummyComponentComponent } from './dummy-component.component';

describe('DummyComponentComponent', () => {
  let component: DummyComponentComponent;
  let fixture: ComponentFixture<DummyComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DummyComponentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DummyComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
