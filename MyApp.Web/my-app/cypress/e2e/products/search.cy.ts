describe('search products', () => {
  beforeEach(() => {
    cy.intercept(/api\/products\/search/).as('searchProducts');
    cy.visit('/#/products');
    cy.wait('@searchProducts');
  });

  it('should display products', () => {
    cy.get('tbody tr').should('have.length', 10);
  });

  it('should search products', () => {
    cy.get('input[name="name"]').type('Generic Metal Keyboard');
    cy.wait('@searchProducts');
    cy.get('tbody tr').should('have.length', 1);
  });

  it('should navigate to create product screen', () => {
    cy.contains('Create').click();
    cy.url().should('contain', 'products/create');
    cy.contains('Create Product');
  });

  it('should navigate to edit product screen', () => {
    cy.contains('Edit').first().click();
    cy.url().should('contain', 'products/dd355b59-e55b-f45e-a02e-6e3fac099863');
    cy.contains('Product "Awesome Concrete Hat"');
  });
});
