describe('Route Edit Form Tests', () => {
    beforeEach(() => {
      cy.visit('/dashboard/edit-route/1'); 
  
      cy.intercept('GET', '/api/routes/1', { fixture: 'route.json' }).as('getRoute');
      cy.intercept('GET', '/api/cars', { fixture: 'cars.json' }).as('getCars');
      cy.intercept('PUT', '/api/routes/1', { statusCode: 200, body: { id: 1, carId: 1, startLocation: 'Updated Start', endLocation: 'Updated End', startTime: '2024-01-01T10:00', endTime: '2024-01-01T12:00', distance: 150, fuelUsed: 12 } }).as('updateRoute');
  

      cy.wait('@getRoute');
      cy.wait('@getCars');
    });
  
    it('should load route data into the form fields', () => {

      cy.get('#carId').should('have.value', '1');
      cy.get('#startLocation').should('have.value', 'Initial Start');
      cy.get('#endLocation').should('have.value', 'Initial End');
      cy.get('#startTime').should('have.value', '2024-01-01T10:00');
      cy.get('#endTime').should('have.value', '2024-01-01T12:00');
      cy.get('#distance').should('have.value', '100');
      cy.get('#fuelUsed').should('have.value', '10');
    });
  
    it('should update the route successfully', () => {

      cy.get('#carId').select('1'); 
      cy.get('#startLocation').clear().type('Updated Start');
      cy.get('#endLocation').clear().type('Updated End');
      cy.get('#startTime').clear().type('2024-01-01T10:00');
      cy.get('#endTime').clear().type('2024-01-01T12:00');
      cy.get('#distance').clear().type('150');
      cy.get('#fuelUsed').clear().type('12');
  
      cy.get('button[type="submit"]').click();
  
      cy.get('.alert-success').should('be.visible');
      cy.get('.alert-success').contains('Route updated successfully');
    });
  
    it('should display error message for invalid data', () => {
      cy.get('#carId').select(''); 
      cy.get('#startLocation').clear(); 
      cy.get('#endLocation').clear(); 
      cy.get('#startTime').clear();
      cy.get('#endTime').clear();
      cy.get('#distance').clear();
      cy.get('#fuelUsed').clear();
  
      cy.get('button[type="submit"]').click();
  
      cy.get('.alert-danger').should('be.visible');
      cy.get('.alert-danger').contains('Please fill out all required fields');
    });
  
    it('should navigate back to the create route form', () => {
      cy.get('button.btn-secondary').click();
  
      cy.url().should('include', '/dashboard');
    });
  });
  