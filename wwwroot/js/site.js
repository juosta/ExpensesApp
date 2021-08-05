// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const allTabs = document.querySelectorAll('.tab');

function activeTab(tab, list) {
    for (const item in list) {
        item.classList.remove('show');
    }
    tab.classList.add('show');
}
const tabbtn1 = document.querySelector("#tab-btn-1");
const tabbtn2 = document.querySelector("#tab-btn-2");
const tab1 = document.querySelector("#tab-1");

const tab2 = document.querySelector("#tab-btn-2");
tabbtn1.addEventListener('click', () => {
    const allTabs = document.querySelectorAll('.tab');
    for (const item of allTabs) {
        item.classList.remove('show');
    }
    const tab1 = document.querySelector("#tab-1");
    tab1.classList.add('show');
});
tabbtn2.addEventListener('click', () => {
    const allTabs = document.querySelectorAll('.tab');
    for (const item of allTabs) {
        item.classList.remove('show');
    }
    const tab2 = document.querySelector("#tab-2");
    tab2.classList.add('show');
});

