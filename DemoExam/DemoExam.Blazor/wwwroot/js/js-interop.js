function set(key, value) {
    localStorage.setItem(key, value);
}

function get(key) {
    return localStorage.getItem(key);
}

function remove(key) {
    return  localStorage.removeItem(key);
}

function closeModal() {
    const closeBtn = document.querySelector('.btn-close');
    closeBtn.click();
}

function showModal(modalId) {
    const modal = new bootstrap.Modal(`#${modalId}`);
    modal?.show();
}