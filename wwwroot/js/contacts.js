const app = Vue.createApp({
    components: { 'default-header': header_component}
})

app.component("map-block",{
    template: `
        <div class="map">
            <iframe src="https://yandex.ru/map-widget/v1/?um=constructor%3A02ef6f28f4c0686b250f2a29877e2ef15303e918d710f0369eba25c95444727d&amp;source=constructor" width="580" height="480" frameborder="0"></iframe>
        </div>
    `
})

app.component("contacts-block", {
    template: `
        <div class="contacts-block" >
            <p>Астраханский государственный университет, 2021</p>
            <p>Адрес: ул. Татищева, 20а, Телефон: (8512) 24-66-89.</p>
            <p>Пн – Пт: 09:00 – 17:30</p>
            <a href="https://www.instagram.com/school_sport_30/" target="_blank">Instagram school_sport_30</a>
        </div>
    `
})

app.component("contacts-info", {
    template: `
        <div class="contacts-container">
            <contacts-block></contacts-block>
            <map-block></map-block>
        </div>
    `
})

app.mount("#app")