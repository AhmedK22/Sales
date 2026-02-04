import { debounce } from './debounce.js';
const itemsContainer = document.getElementById('items');
const addItemBtn = document.getElementById('addItem');

function generateGuid() {
    if (typeof crypto !== 'undefined' && crypto.randomUUID) return crypto.randomUUID();
    const s4 = () => Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1);
    return `${s4()}${s4()}-${s4()}-${s4()}-${s4()}-${s4()}${s4()}${s4()}`;
}

const customerIdInput = document.getElementById('customerId');
if (customerIdInput && !customerIdInput.value) {
    customerIdInput.value = generateGuid();
}
function addRow() {
    const idx = itemsContainer.children.length;
    const div = document.createElement('div');
    div.innerHTML = `
        <label for='product-${idx}'>Product</label>
        <input id='product-${idx}' name='Items[${idx}].ProductName' placeholder='Product name' required minlength='2' />
        <label for='price-${idx}'>Unit Price</label>
        <input id='price-${idx}' name='Items[${idx}].UnitPrice' type='number' step='0.01' min='0.01' value='0.01' required />
        <label for='qty-${idx}'>Quantity</label>
        <input id='qty-${idx}' name='Items[${idx}].Quantity' type='number' min='1' value='1' required />
        <button type='button' class='remove'>Remove</button>
    `;
    itemsContainer.appendChild(div);
    div.querySelector('.remove')?.addEventListener('click', () => { div.remove(); });
}
addItemBtn.addEventListener('click', addRow);
addRow();

document.getElementById('orderForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    const errorBox = document.getElementById('formError');
    errorBox.textContent = '';

    const form = e.target;
    const formData = new FormData(form);
    const obj = {
        CustomerId: formData.get('CustomerId'),
        Items: []
    };
    const entries = Array.from(formData.entries());
    const itemsMap = {};
    entries.forEach(([k, v]) => {
        const m = k.match(/Items\[(\d+)\]\.(.+)/);
        if (m) {
            const idx = m[1];
            const prop = m[2];
            itemsMap[idx] = itemsMap[idx] || {};
            itemsMap[idx][prop] = prop === 'ProductName' ? v : Number(v);
        }
    });
    obj.Items = Object.values(itemsMap);

    const customerId = String(obj.CustomerId || '').trim();
    obj.CustomerId = customerId;
    const guidRegex = /^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$/;
    if (!customerId) {
        errorBox.textContent = 'Customer Id is required.';
        return;
    }
    if (!guidRegex.test(customerId)) {
        errorBox.textContent = 'Customer Id must be a valid GUID.';
        return;
    }
    if (!obj.Items.length) {
        errorBox.textContent = 'Please add at least one item.';
        return;
    }
    if (obj.Items.some(i => !i.ProductName || i.ProductName.length < 2)) {
        errorBox.textContent = 'Each item must have a product name (min 2 characters).';
        return;
    }
    if (obj.Items.some(i => !i.UnitPrice || i.UnitPrice < 0.01)) {
        errorBox.textContent = 'Each item must have a unit price greater than 0.';
        return;
    }
    if (obj.Items.some(i => !i.Quantity || i.Quantity < 1)) {
        errorBox.textContent = 'Each item must have a quantity of at least 1.';
        return;
    }
    obj.DiscountStrategy = document.getElementById('discountSelect').value;
    const res = await fetch('http://localhost:5045/api/orders', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(obj)
    });
    if (!res.ok) {
        const text = await res.text();
        errorBox.textContent = text || 'Failed to create order.';
        return;
    }
    location.href = '/Orders';
});
