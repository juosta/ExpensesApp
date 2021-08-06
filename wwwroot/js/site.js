// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// Show/hide tabs in home transactions list
const allTabBtns = document.querySelectorAll('[id^="tab-btn"]');
for (const tabBtn of allTabBtns) {
    tabBtn.addEventListener('click', () => {
        const allTabs = document.querySelectorAll('.tab');
        for (const item of allTabBtns) {
            item.classList.remove('active');
        }
        tabBtn.classList.add('active');
        for (const item of allTabs) {
            item.classList.remove('show');
        }
        const whichToShow = tabBtn.id.substr(-1);
        const showTab = document.querySelector(`#tab-${whichToShow}`);
        showTab.classList.add('show');
    });
}
