export function debounce(fn, wait = 300) {
    let t;
    return (...args) => {
        clearTimeout(t);
        t = setTimeout(() => fn(...args), wait);
    };
}

export function initList(apiUrl, inputSelector, targetSelector) {
    const input = document.querySelector(inputSelector);
    const target = document.querySelector(targetSelector);
    const doSearch = async (q) => {
        const res = await fetch(apiUrl + (q ? '?search=' + encodeURIComponent(q) : ''));
        const html = await res.text();
        target.innerHTML = html;
    };
    input.addEventListener('input', debounce(e => doSearch(e.target.value), 400));
}