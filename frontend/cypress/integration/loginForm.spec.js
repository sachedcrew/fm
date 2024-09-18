describe('Login Form Tests', () => {
    beforeEach(() => {
      cy.visit('/login');
    });
  
    it('should login successfully and redirect to dashboard', () => {
      cy.get('#email').type('valid@example.com');
      cy.get('#password').type('validpassword');
      cy.get('form').submit();
  
      cy.url().should('include', '/dashboard');
    });
  
    it('should display error message on login failure', () => {
      cy.get('#email').type('invalid@example.com');
      cy.get('#password').type('wrongpassword');
      cy.get('form').submit();
  
      cy.get('.alert-danger').should('be.visible');
      cy.get('.alert-danger').contains('Invalid email or password');
    });
  });
  