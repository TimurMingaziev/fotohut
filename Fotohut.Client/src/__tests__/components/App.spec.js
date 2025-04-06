// @vitest-environment happy-dom
import { describe, it, expect } from 'vitest'
import { mount } from '@vue/test-utils'
import App from '../../App.vue'

describe('App Component', () => {
  it('renders correctly', () => {
    const wrapper = mount(App)
    expect(wrapper.exists()).toBe(true)
  })

  it('has the correct title', () => {
    const wrapper = mount(App)
    expect(wrapper.find('h1').text()).toBe('Fotohut')
  })

  it('has a welcome message', () => {
    const wrapper = mount(App)
    expect(wrapper.find('h2').text()).toBe('Welcome to Fotohut')
  })

  it('has a logo', () => {
    const wrapper = mount(App)
    const logo = wrapper.find('img.logo')
    expect(logo.exists()).toBe(true)
    expect(logo.attributes('alt')).toBe('Vue logo')
  })
}) 